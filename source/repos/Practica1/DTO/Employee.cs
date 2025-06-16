namespace DTO
{
    public class Employee
    {
        public int Id { get; set; }
        public string SecurityId { get; set; }
        public string Name { get; set; }
        public string Alias { get; set; }
        public string LastName { get; set; }
        public string SurName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public DateTime HiringDate { get; set; }
        public string Status { get; set; }
        public int? ManagerId { get; set; }
    }
}