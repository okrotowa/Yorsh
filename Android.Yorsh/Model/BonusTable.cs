using SQLite;

namespace Android.Yorsh.Model
{
    public class BonusTable
    {
        public BonusTable()
        {
            
        }
        public BonusTable(string bonusName, int score)
        {
            BonusName = bonusName;
            Score = score;
        }

        [PrimaryKey, AutoIncrement]
        public int Id { get; set; }
        public string BonusName { get; set; }
        public int Score { get; set; }
        public override string ToString()
        {
            return "BonusTable";
        }
    }
}