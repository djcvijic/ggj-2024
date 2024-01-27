namespace RuleSystem
{
    public class CatData
    {
        public readonly int rowNumber;
        public readonly int seatNumber;
        public readonly CatColor color;
        public readonly CatBuild build;
        public readonly CatAge age;
        public readonly CatStatus status;
        public readonly CatGender gender;

        public CatData(
            int rowNumber,
            int seatNumber,
            CatColor color,
            CatBuild build,
            CatAge age,
            CatStatus status,
            CatGender gender)
        {
            this.rowNumber = rowNumber;
            this.seatNumber = seatNumber;
            this.color = color;
            this.build = build;
            this.age = age;
            this.status = status;
            this.gender = gender;
        }
    }
}