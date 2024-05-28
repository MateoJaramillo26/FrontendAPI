namespace frontend.Models
{
    public class SRIData
    {
        private readonly bool _isContributor;
        public string IsContributor { get => _isContributor ? "Sí" : "No"; }
    }
}
