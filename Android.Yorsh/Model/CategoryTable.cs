using SQLite;

namespace Android.Yorsh.Model
{
    public class CategoryTable
    {
        public CategoryTable()
        {

        }

        public CategoryTable(int id, string categoryName,string imageName)
        {
            Id = id;
            ImageName = imageName;
            CategoryName = categoryName;
        }

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string ImageName { get; set; }

        public string CategoryName { get; set; }
        public override string ToString()
        {
            return "CategoryTable";
        }
    }
}