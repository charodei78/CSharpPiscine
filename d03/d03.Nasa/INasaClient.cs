using System.Threading.Tasks;
using d03.Nasa.Apod.Models;

namespace d03.Nasa
{
    public interface INasaClient<in TIn, out TOut>
    {
        public TOut GetAsync(TIn input);
    }
}