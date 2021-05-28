using System;
using static System.Console;
using static System.DateTime;
using static System.Int32;


static double MonthInterestRate(double yearInterestRate)
{
    return yearInterestRate / 12 / 100;
}

static double AnnuityPayment( double creditAmount, int monthCount, double yearInterestRate )
{
    double result = 0;
    double monthInterestRate = MonthInterestRate(yearInterestRate);

    result = creditAmount * monthInterestRate * Math.Pow(1 + monthInterestRate, monthCount);
    result /= Math.Pow(1 + monthInterestRate, monthCount) - 1;
    
    return result;
}

static double MonthlyPaymentPrecent(double creditAmount, double interestRate, DateTime date)
{
    int daysCount = DateTime.DaysInMonth(date.Year, date.Month);
    int daysInYear = DateTime.IsLeapYear(date.Year) ? 366 : 365;

    double result = (creditAmount * interestRate * daysCount) / (100 * daysInYear);
    return result;
}

static int NumberOfMothCredit(double paymentAmount, double creditAmount, double interestRate)
{
    double i = MonthInterestRate(interestRate);
    return (int) Math.Log(paymentAmount / (paymentAmount - i * creditAmount), 1 + i);
}

static void PrintResult(double overpaymentLessPayment, double overpaymentLessTerm)
{
    Console.WriteLine($"Переплата при уменьшении платежа: {overpaymentLessPayment:f2}р.");
    Console.WriteLine($"Переплата при уменьшении срока: {overpaymentLessTerm:f2}р.");
    
    string result;
    double diff = Math.Abs(overpaymentLessTerm - overpaymentLessPayment);

    result = diff switch
    {
        0 => "Переплата одинакова в обоих вариантах.",
        < 0 => $"Уменьшение платежа выгоднее уменьшения срока на {diff:f2}р.",
        _ => $"Уменьшение срока выгоднее уменьшения платежа на {diff:f2}р."
    };

    Console.WriteLine(result);

}


//  Сумма кредита, р
double creditAmount;

//  Годовая процентная ставка, %
double yearInterestRate;

//  Количество месяцев кредита
int numberOfMoth;

//  Номер месяца кредита, в котором вносится досрочный платёж
int earlyPaymentMonth;

//  Сумма досрочного платежа, р
double earlyPaymentAmount;



try
{
    creditAmount = double.Parse(args[0]);
    yearInterestRate = double.Parse(args[1]);
    numberOfMoth = int.Parse(args[2]);
    earlyPaymentMonth = int.Parse(args[3]);
    earlyPaymentAmount = double.Parse(args[4]);

    if (creditAmount <= 0 ||
        yearInterestRate < 0 ||
        numberOfMoth <= 0 ||
        earlyPaymentMonth < 0)
    {
        throw new Exception();
    }
}
catch (Exception)
{
    Console.WriteLine("Ошибка ввода. Проверьте входные данные и повторите запрос.");
    return 1;
}


static double calculateLessPayment(int numberOfMoth, int yearInterestRate, double creditAmount, DateTime date, bool lessPayment = true, int earlyPaymentMonth = 0, double earlyPaymentAmount = 0)
{
    double precentAmount, annuityPayment, debet = 0, sum = 0;

    string title = lessPayment ? "Рассчет с уменьшением суммы платежей" : "Рассчет с уменьшением количества месяцев";
    
    Console.WriteLine($"------[  {title}  ]------------------------------------");
    Console.WriteLine($"{"Дата", -8}{"Платеж", 19}{"ОД", 17}{"Проценты", 26}{"Остаток", 20}");
    Console.WriteLine("---------------------------------------------------------------------------------------------------");

    annuityPayment = AnnuityPayment(creditAmount, numberOfMoth, yearInterestRate);

    for (int month = 1; month <= numberOfMoth && creditAmount > 0; month++)
    {
        
        precentAmount = MonthlyPaymentPrecent(creditAmount, yearInterestRate, date);
        date = date.AddMonths(1);


        if (creditAmount > 0)
        {
            debet = annuityPayment - precentAmount;
            creditAmount -= debet;
            sum += annuityPayment;
        }
       

        if (month == earlyPaymentMonth)
        {
            creditAmount -= earlyPaymentAmount;
            sum += earlyPaymentAmount;
            if (lessPayment)
            {
                annuityPayment = AnnuityPayment(creditAmount, numberOfMoth - earlyPaymentMonth, yearInterestRate);
            }
            else
            {
                numberOfMoth = NumberOfMothCredit(annuityPayment, creditAmount, yearInterestRate) + earlyPaymentMonth + 1;
            }
        }
        
        Console.WriteLine($"{date:d}{'|', 10}{annuityPayment, 10 :f2}{'|', 10}{debet, 10 :f2}{'|', 10}{precentAmount, 10 :f2}{'|', 10}{(creditAmount > 0 ? creditAmount : 0), 10 :f2}");
    }

    sum += creditAmount;
    Console.WriteLine("---------------------------------------------------------------------------------------------------");
    Console.WriteLine();
    
    return sum;
}

double overpaymentLessPayment, overpaymentLessTerm;

var date = new DateTime(2021, 05, 01);

overpaymentLessPayment = -creditAmount +
    calculateLessPayment(numberOfMoth, (int)yearInterestRate, creditAmount, date, true, earlyPaymentMonth, earlyPaymentAmount);

overpaymentLessTerm = -creditAmount +
    calculateLessPayment(numberOfMoth, (int)yearInterestRate, creditAmount, date, false, earlyPaymentMonth, earlyPaymentAmount);

PrintResult( overpaymentLessPayment, overpaymentLessTerm );

return 0;