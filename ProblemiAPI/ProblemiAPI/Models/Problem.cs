namespace ProblemiAPI.Models
{
    public class Problem
    {
        public long Id { get; set; }
        public string Name { get; set; }
        //public DateTime Date { get; set; }
        public string? Description { get; set; }
        public string? PossibleSolution { get; set; }
        public bool IsSolved { get; set; }
    }
}
