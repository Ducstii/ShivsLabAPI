namespace ShivsLabAPI
{
    public class Config
    {
        public bool Enabled { get; set; } = true;
        public bool Debug { get; set; } = false;
        public int SuccessChance { get; set; } = 10;
        public int DamageAmount  { get; set; } = 1;
        public int ShivDamageAmount { get; set; } = 40;
        public float Range { get; set; } = 1;
    }
}