namespace ECERP.Models.Entities
{
    using System.ComponentModel.DataAnnotations;
    using Core;

    public abstract class AccountBalance<T> : Entity<int> where T : struct
    {
        public int Year { get; set; }

        [Required]
        public T BeginningBalance { get; set; }

        public T Balance1 { get; set; }
        public T Balance2 { get; set; }
        public T Balance3 { get; set; }
        public T Balance4 { get; set; }
        public T Balance5 { get; set; }
        public T Balance6 { get; set; }
        public T Balance7 { get; set; }
        public T Balance8 { get; set; }
        public T Balance9 { get; set; }
        public T Balance10 { get; set; }
        public T Balance11 { get; set; }
        public T Balance12 { get; set; }
    }
}