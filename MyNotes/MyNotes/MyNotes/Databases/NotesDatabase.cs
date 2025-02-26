﻿using System.Collections.Generic;
using System.Threading.Tasks;
using SQLite;

namespace MyNotes.Models
{
    /// <summary>
    /// База данных заметок
    /// </summary>
    public class NotesDatabase
    {
        SQLiteAsyncConnection database;

        /// <summary>
        /// Создание базы данных
        /// </summary>
        /// <param name="dbPath">Путь к базе данных</param>
        public NotesDatabase(string dbPath)
        {
            database = new SQLiteAsyncConnection(dbPath);
            database.CreateTableAsync<Note>().Wait();
        }

        /// <summary>
        /// Метод получения содержимого базы данных
        /// </summary>
        /// <returns>Содержимое базы данных</returns>
        public Task<List<Note>> GetNotesAsync()
        {
            return database.Table<Note>().ToListAsync();
        }

        /// <summary>
        /// Метод сохранения заметок в базе данных
        /// </summary>
        /// <param name="note">Заметка</param>
        /// <returns>База данных с сохраненной заметкой</returns>
        public Task<int> SaveNoteAsync(Note note)
        {
            // Обновляем, если такая заметка уже есть.
            if (note.ID != 0)
                return database.UpdateAsync(note);
            else
                return database.InsertAsync(note);
        }

        /// <summary>
        /// Удаление заметки из базы даных
        /// </summary>
        /// <param name="id">ID заметки</param>
        /// <returns>База данных с удаленной заметкой</returns>
        public Task<int> DeleteNoteAsync(int id)
        {
            return database.DeleteAsync<Note>(id);
        }
    }
}
