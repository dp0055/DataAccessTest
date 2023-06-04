namespace DataAccessTest.Models
{
    public class FilterModel
    {
        /// <summary>
        /// Filter data with these car names.
        /// </summary>
        public string[] CarNames { get; set; } 
        /// <summary>
        /// Only bring back data with films that begin with these letters.
        /// </summary>
        public char[] FilmsBeginningWithLetters { get; set; } 
    }
}
