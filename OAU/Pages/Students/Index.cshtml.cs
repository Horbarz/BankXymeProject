using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using OAU.Data;
using OAU.Models;

namespace OAU.Pages.Students
{
    public class IndexModel : PageModel
    {
        private readonly OAU.Data.StudentContext _context;

        public IndexModel(OAU.Data.StudentContext context)
        {
            _context = context;
        }

        //Sorting students
        public string NameSort{get; set;}
        public string DateSort { get; set; }
        public string CurrentFilter { get; set; }
        public string CurrentSort { get; set; }
        public IList<Student> Student { get;set; }

        public async Task OnGetAsync(string sortOrder)
        {
            NameSort = String.IsNullOrEmpty(sortOrder)?"name_desc":"";
            DateSort = sortOrder == "Date"? "date_desc":"";

            IQueryable<Student> studentsIQ = from s in _context.Students select s;

            switch (sortOrder)
            {
                case "name_desc":
                    studentsIQ = studentsIQ.OrderByDescending(s => s.LastName);
                    break;
                case "Date":
                    studentsIQ = studentsIQ.OrderBy(s => s.EnrollmentDate);
                    break;
                case "date_desc":
                    studentsIQ = studentsIQ.OrderByDescending(s => s.EnrollmentDate);
                    break;
                default:
                    studentsIQ = studentsIQ.OrderBy(s => s.LastName);
                    break;
            }

            Student = await studentsIQ.AsNoTracking().ToListAsync();
        }
    }
}
