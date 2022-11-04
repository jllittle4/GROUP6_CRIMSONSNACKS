namespace api.Models
{
    //model "departments" table in database
    public class Department
    {
        public int DepId { get; set; }
        public string DepName { get; set; }

        public override string ToString()
        {
            return $"ID\tDepartment\n{this.DepId}\t{this.DepName}";
        }
    }
}