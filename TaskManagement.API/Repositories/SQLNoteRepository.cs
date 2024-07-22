using Microsoft.EntityFrameworkCore;
using TaskManagement.API.Data;
using TaskManagement.API.Models.Domain;

namespace TaskManagement.API.Repositories
{
    public class SQLNoteRepository : INoteRepository
    {
        private readonly TaskManagementSystemDbContext dbContext;

        public SQLNoteRepository(TaskManagementSystemDbContext dbContext)
        {
            this.dbContext = dbContext;
        }
        

        public async Task<List<Note>> GetAllAsync()
        {
            return await dbContext.Notes.ToListAsync();
        }

        public async Task<Note?> GetByIdAsync(Guid id)
        {
            return await dbContext.Notes.FirstOrDefaultAsync(x => x.Id == id);
        }

        public async Task<Note> CreateAsync(Note note)
        {
            await dbContext.Notes.AddAsync(note);
            await dbContext.SaveChangesAsync();

            return note;
        }

        public async Task<Note?> UpdateAsync(Guid id, Note note)
        {
            var existingNote = await dbContext.Notes.FirstOrDefaultAsync(x =>x.Id == id);

            if (existingNote == null) { return null; }

            existingNote.Comments = note.Comments;
            existingNote.EmpTaskId = note.EmpTaskId;

            await dbContext.SaveChangesAsync();

            return existingNote;
        }

        public async Task<Note?> DeleteAsync(Guid id)
        {
            var existingNote = await dbContext.Notes.FirstOrDefaultAsync(x =>x.Id == id);

            if (existingNote == null) { return null; }

            dbContext.Notes.Remove(existingNote);
            await dbContext.SaveChangesAsync();

            return existingNote;
        }
    }
}
