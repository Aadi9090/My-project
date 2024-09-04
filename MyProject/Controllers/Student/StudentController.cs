using MyProject.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MyProject.Models.Student;
using Microsoft.AspNetCore.Authorization;
using MyProject.Services;

namespace MyProject.Controllers.Student
{
    [Authorize]
    public class StudentController : Controller
    {


      
            private readonly ApplicationDbContext context;

            public StudentController(ApplicationDbContext context)
            {
                this.context = context;
            }
        
            [HttpGet]
            public async Task<IActionResult> Index()
            {
                try
                {
                    var students = await context.Students.ToListAsync();
                    return View(students);
                }
                catch (Exception ex)
                {
                  
                    TempData["error"] = $"An error occurred while fetching Student: {ex.Message}";
             
                    return StatusCode(500, $"Error in getting Student details.{ex.Message}");
                }
            }

            public async Task<IActionResult> Details(int id)
            {
                try
                {
                   
                    var Student = await context.Students
                        .FirstOrDefaultAsync(c => c.StudentID == id);

                 
                    if (Student == null)
                    {
                        return NotFound($"Studnet with ID {id} not found.");
                    }

                
                    return View(Student);
                }
                catch (Exception ex)
                {
                   
                    TempData["error"] = $"An error occurred while fetching the Student: {ex.Message}";
                
                    return StatusCode(500, $"Error in getting Student details: {ex.Message}");
                }
            }

            [HttpGet]
            public IActionResult Create()
            {
                return View();
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Create(StudentModel student)
            {
                if (!ModelState.IsValid)
                {
                    return View(student);
                }


                try
                {
                    var existingUser = await context.Students
                .SingleOrDefaultAsync(u => u.Email == student.Email);

                    if (existingUser != null)
                    {
                   
                        TempData["msg"] = "User already exists.";
                        return View(student); 
                    }


                    context.Students.Add(student);
                    await context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
              
                    ModelState.AddModelError("", "An error occurred while creating the student. Please try again.");
                    return View(student);
                }
            }


            [HttpGet]
            public async Task<IActionResult> Edit(int id)
            {
                try
                {
                    var student = await context.Students.FindAsync(id);
                    if (student == null)
                    {
                        return NotFound();
                    }
                    return View(student);
                }
                catch (Exception ex)
                {
                   
                    return StatusCode(500, "Internal server error.");
                }
            }

            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Edit(StudentModel student)
            {
                if (!ModelState.IsValid)
                {
                    return View(student);
                }

                try
                {
                    context.Students.Update(student);
                    await context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
                catch (Exception ex)
                {
                   
                    ModelState.AddModelError("", "An error occurred while updating the student. Please try again.");
                    return View(student);
                }
            }


            public async Task<IActionResult> Delete(int id)
            {
                try
                {
                    var student = await context.Students.SingleOrDefaultAsync(e => e.StudentID == id);
                    if (student != null)
                    {
                     
                        context.Students.Remove(student);
                        await context.SaveChangesAsync();
                        TempData["delete"] = "Record deleted";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["error"] = "Record not found";
                        return RedirectToAction("Index");
                    }
                }
                catch (Exception ex)
                {
           
                    return StatusCode(500, "Internal server error.");
                }
            }
        }

    
}
