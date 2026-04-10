namespace WebAplicationAPIRestDemo.DAL.Model
{
    public class Character
    {
        public int idCharacter { get; set; }
        public string nameCharacter { get; set; } = "";
        public int healthCharacter { get; set; }
        public int levelCharacter { get; set; } 
        public int skillTreePath { get; set; }
        public int teamPosition { get; set; }
        public bool isEnemy { get; set; }
    }
}
