namespace SignRecognition.Interfaces
{
    interface IPredictionLocation
    {
        string Class { get; set; }
        double Latitude { get; set; }
        double Longitude { get; set; }
    }
}
