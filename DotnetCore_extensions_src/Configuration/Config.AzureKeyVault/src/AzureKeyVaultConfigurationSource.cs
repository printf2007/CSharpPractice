// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

namespace Microsoft.Extensions.Configuration.AzureKeyVault
{
    /// <summary>
    /// Represents Azure KeyVault secrets as an <see cref="IConfigurationSource"/>.
    /// </summary>
    internal class AzureKeyVaultConfigurationSource : IConfigurationSource
    {
        private readonly AzureKeyVaultConfigurationOptions _options;

        public AzureKeyVaultConfigurationSource(AzureKeyVaultConfigurationOptions options)
        {
            _options = options;
        }

        /// <inheritdoc />
        public IConfigurationProvider Build(IConfigurationBuilder builder)
        {
            return new AzureKeyVaultConfigurationProvider(new KeyVaultClientWrapper(_options.Client), _options.Vault, _options.Manager, _options.ReloadInterval);
        }
    }
}
