using System;
using System.Reflection;
using Microsoft.AspNetCore.Http;

var type = typeof (DefaultHttpContext);

var responsePropString = type.GetProperty("Response").ToString();

Console.WriteLine(responsePropString);

var responseFieldInfo = type.GetField("_response", BindingFlags.Instance | BindingFlags.NonPublic);
Console.WriteLine(responseFieldInfo);

DefaultHttpContext context = new();
Console.WriteLine($"Old Response value: {context.Response}");

responseFieldInfo.SetValue(context, null);
Console.WriteLine($"New Response value: {context.Response}");

