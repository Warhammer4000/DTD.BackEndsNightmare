

namespace DTD.BackEndsNightmare.DataModels
{
    public class ProjectConfiguration
    {
        private readonly string _baseUrl;
        private readonly bool _isVersioned;
        private readonly string _versionSuffix;
        private string _versionNumber;

        public ProjectConfiguration(string baseUrl, bool isVersioned=false, string versionSuffix="v", string versionNumber="1")
        {
            _baseUrl = baseUrl;
            _isVersioned = isVersioned;
            _versionSuffix = versionSuffix;
            _versionNumber = versionNumber;
        }

        public void ChangeVersion(string newVersion)
        {
            _versionNumber = newVersion;
        }

        public string BaseUrl => !_isVersioned ? _baseUrl : string.Concat(_baseUrl, _versionSuffix + _versionNumber,"/");
    }
}
