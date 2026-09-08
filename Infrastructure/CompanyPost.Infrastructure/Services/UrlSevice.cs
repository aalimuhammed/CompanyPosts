using CompanyPost.Infrastructure.Settings;

namespace CompanyPost.Infrastructure.Services
{
    internal sealed class UrlSevice:IUrlService
    {
        private readonly UrlSettings _urlSettings;
        public UrlSevice(IOptions<UrlSettings> options)
        {
            _urlSettings = options.Value;
        }
        public string GetLocalUrl()
        {
            return _urlSettings.LocalUrl;
        }
        public string GetProductionUrl()
        {
            return _urlSettings.ProductionUrl;
        }
    }
}
