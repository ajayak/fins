using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Threading.Tasks;
using FINS.Context;
using FINS.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace FINS.UnitTest
{
    /// <summary>
    /// Inherit from this type to implement tests
    /// that make use of the in-memory test database
    /// context.
    /// </summary>
    public abstract class InMemoryContextTest : TestBase
    {
        /// <summary>
        /// Gets the in-memory database context.
        /// </summary>
        protected FinsDbContext Context { get; private set; }
        protected UserManager<ApplicationUser> UserManager { get; }
        protected RoleManager<IdentityRole> RoleManager { get; }

        protected InMemoryContextTest()
        {
            Context = ServiceProvider.GetService<FinsDbContext>();
            UserManager = ServiceProvider.GetService<UserManager<ApplicationUser>>();
            RoleManager = ServiceProvider.GetService<RoleManager<IdentityRole>>();

            LoadTestData();
        }

        /// <summary>
        /// Override this method to load test data
        /// into the in-memory database context prior
        /// to any tests being executed in your
        /// test class.
        /// </summary>
        protected virtual void LoadTestData()
        {
        }

        /// <summary>
        /// Override this method to load test data
        /// into the in-memory database context prior
        /// to any tests being executed in your
        /// test class.
        /// FRAGILE: this method can't be called from the constructor so you must call it manually
        /// </summary>
        protected virtual async Task LoadTestDataAsync()
        {
            await Task.Delay(0); //To prevent compiler warning
        }
    }
}
