namespace APIProjecte.DAL.Model
{
    
    public class Skill
    {
        public int idSkill { get; set; }
        public string nameSkill { get; set; }
        public string descriptionSkill { get; set; }
        public int baseDamageSkill { get; set; }
        public int energyCostSkill { get; set; }
        public int dotSkill { get; set; }
        public bool isUnlockedSkill { get; set; }
        public int skillTreePathSkill { get; set; }
        public int characterIdSkill { get; set; }
    }
    
}
