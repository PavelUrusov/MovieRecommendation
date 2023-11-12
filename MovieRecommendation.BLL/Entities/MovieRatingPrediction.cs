namespace MovieRecommendation.BLL.Entities;

public class MovieRatingPrediction
{
    public float Label { get; set; }
    public float Score { get; set; }
    public int UserId { get; set; }
    public int MovieId { get; set; }
}