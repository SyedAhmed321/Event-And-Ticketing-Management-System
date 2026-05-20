using Event___Ticketing_Management_System.Configurations;
using Event___Ticketing_Management_System.Interfaces.Repositories;
using Event___Ticketing_Management_System.Models.Admin;
using MongoDB.Driver;

namespace Event___Ticketing_Management_System.Repositories
{
    public class AdminRepository : IAdminRepository
    {
        private readonly IMongoCollection<AdminAction> _actions;
        private readonly IMongoCollection<SystemLog> _logs;

        public AdminRepository(MongoDbService mongoDbService)
        {
            _actions = mongoDbService.Database.GetCollection<AdminAction>("AdminActions");
            _logs = mongoDbService.Database.GetCollection<SystemLog>("SystemLogs");
        }

        // =========================================
        // ✅ CREATE ADMIN ACTION
        // =========================================
        public async Task CreateAdminActionAsync(AdminAction action)
        {
            await _actions.InsertOneAsync(action);
        }

        // =========================================
        // ✅ GET ALL ADMIN ACTIONS
        // =========================================
        public async Task<List<AdminAction>> GetAdminActionsAsync()
        {
            return await _actions
                .Find(_ => true)
                .SortByDescending(a => a.PerformedAt)
                .ToListAsync();
        }

        // =========================================
        // ✅ CREATE SYSTEM LOG
        // =========================================
        public async Task CreateSystemLogAsync(SystemLog log)
        {
            await _logs.InsertOneAsync(log);
        }

        // =========================================
        // ✅ GET SYSTEM LOGS
        // =========================================
        public async Task<List<SystemLog>> GetSystemLogsAsync()
        {
            return await _logs
                .Find(_ => true)
                .SortByDescending(l => l.CreatedAt)
                .ToListAsync();
        }
    }
}