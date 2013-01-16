using System;

namespace Lounge.Core.Configuration
{
    public interface IProvideCouchConfiguration
    {
        Uri ServerUri { get; }
        Uri DatabaseUri { get; }
        string DatabaseName { get; }
    }
}