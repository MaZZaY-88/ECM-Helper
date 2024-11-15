using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace ECMHelper.Controllers
{
    public class ShiftManagementController : Controller
    {
        /// <summary>
        /// Action to get the current shift information.
        /// </summary>
        /// <returns>Returns the current shift view with details.</returns>
        [HttpGet("Current")] // Route: /ShiftManagement/Current
        public IActionResult CurrentShift()
        {
            try
            {
                LoggerService.LogInfo("Fetching current shift information.");

                // Example shift data
                var shiftInfo = new
                {
                    ShiftId = 1,
                    Name = "Day Shift",
                    StartTime = "08:00",
                    EndTime = "16:00"
                };

                LoggerService.LogInfo("Successfully fetched current shift information.");
                ViewBag.ShiftInfo = shiftInfo;
                return View("Index"); // Reuse the same view as HomeController (Index.cshtml)
            }
            catch (Exception ex)
            {
                LoggerService.LogError("Error occurred while fetching current shift information.", ex);
                return StatusCode(500, "An error occurred while fetching the current shift information.");
            }
        }

        /// <summary>
        /// Action to get the list of current back-to-back shifts for the company.
        /// </summary>
        /// <returns>Returns the view with the list of current back-to-back shifts.</returns>
        [HttpGet("BackToBack")] // Route: /ShiftManagement/BackToBack
        public IActionResult BackToBackShifts()
        {
            try
            {
                LoggerService.LogInfo("Fetching list of current back-to-back shifts.");

                // Example list of back-to-back shifts (actual implementation would involve retrieving from a database)
                var backToBackShifts = new List<object>
                {
                    new { Group = "Day Shift", FirstEmployee = "John Doe", SecondEmployee = "Jane Smith", NextShiftDate = "2024-11-20" },
                    new { Group = "Night Shift", FirstEmployee = "Alice Brown", SecondEmployee = "Bob White", NextShiftDate = "2024-11-21" }
                };

                LoggerService.LogInfo("Successfully fetched list of current back-to-back shifts.");
                ViewBag.BackToBackShifts = backToBackShifts;
                return View("BackToBackShifts"); // Use a specific view for back-to-back shifts
            }
            catch (Exception ex)
            {
                LoggerService.LogError("Error occurred while fetching back-to-back shifts.", ex);
                return StatusCode(500, "An error occurred while fetching the back-to-back shifts.");
            }
        }
    }
}
