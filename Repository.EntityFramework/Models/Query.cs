using System.ComponentModel.DataAnnotations;

namespace Repositories.EntityFramework.Models
{
    public class Query<T> : IBaseQueryValidator, IQuery<T> where T : class
    {
        [Range(1, int.MaxValue)]
        public int Page { get; set; } = 1;

        [Range(5, 100)]
        public int Size { get; set; } = 5;

        public string SortBy { get; set; }

        public string SortDirection { get; set; } = SortDirectionEnum.DESC.ToString();

        public bool AreBaseParametersValid()
        {
            if (!SortDirection.Equals(SortDirectionEnum.ASC.ToString()) &&
                !SortDirection.Equals(SortDirectionEnum.DESC.ToString()))
            {
                return false;
            }

            return true;
        }
    }

    public interface IQuery<T> where T : class
    {
        int Page { get; set; }

        int Size { get; set; }

        string SortBy { get; set; }

        string SortDirection { get; set; }
    }

    public interface IBaseQueryValidator
    {
        bool AreBaseParametersValid();
    }   
}
