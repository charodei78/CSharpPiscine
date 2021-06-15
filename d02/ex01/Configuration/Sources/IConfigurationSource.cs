using System.Collections;

namespace ex01.Configuration.Sources
{
    public interface IConfigurationSource
    {
        string Path { get; }

        Hashtable ToHash();
    }
}