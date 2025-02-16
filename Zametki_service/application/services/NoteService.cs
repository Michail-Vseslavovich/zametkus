using Zametki_service.domain;
using Zametki_service.domain.entity;
using Zametki_service.infrastructure.DBcontext;

namespace Zametki_service.application.services
{
    public class NoteService(NoteBDcontext db) : Iservice<Note>
    {
        public Note getById(int id)
        {
            return db.Notes.First(p => p.Id == id);
        }
        public List<Note> GetAll(int UserId)
        {
            return db.Notes.Where(note => note.RelatedUserId == UserId).ToList();
        }
        public bool DeleteById(int id)
        {
            Note? NoteToDelete = db.Notes.FirstOrDefault(note => note.Id == id);
            if (NoteToDelete != null)
            {
                db.Notes.Remove(NoteToDelete);
                db.SaveChanges();
                return true;
            }
            return false;
        }
        public bool Add(Note NoteToAdd)
        {
            if (NoteToAdd != null && (db.Notes.FirstOrDefault(Note => Note.Id == NoteToAdd.Id) == null))
            {
                db.Notes.Add(NoteToAdd);
                db.SaveChanges();
                return true;
            }
            return false;
        }
        public bool Update(Note NewNote)
        {
            if (NewNote != null)
            {
                db.Notes.Update(NewNote);
                db.SaveChanges();
                return true;
            }
            return false;
        }
    }
}
