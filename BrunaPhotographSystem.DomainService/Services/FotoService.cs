using BrunaPhotographSystem.DomainModel.Entities;
using BrunaPhotographSystem.DomainModel.Interfaces.Repositories;
using BrunaPhotographSystem.DomainModel.Interfaces.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BrunaPhotographSystem.DomainService.Services
{
    public class FotoService : IFotoService
    {
        private readonly IFotoRepository _fotoRepository;
        private readonly IAlbumService _albumService;

        public FotoService(IFotoRepository fotoRepository, IAlbumService albumService)
        {
            _fotoRepository = fotoRepository;
            _albumService = albumService;
        }

        public void Criar(Foto entity)
        {
            _fotoRepository.Create(entity);
        }

        public void Deletar(Guid id)
        {
            _fotoRepository.Delete(id);
        }

        public Foto Buscar(Guid id)
        {
            return _fotoRepository.Read(id);
        }

        public IEnumerable<Foto> BuscarTodos()
        {
            return _fotoRepository.ReadAll();
        }

        public void Atualizar(Foto entity)
        {
            _fotoRepository.Update(entity);
        }
        public IEnumerable<Foto> BuscarTodosDoAlbumPeloNome(String album)
        {
            return BuscarTodos().Where(foto => foto.Album.Id == _albumService.BuscarPeloNome(album).Id);
        }
        public IEnumerable<Foto> BuscarTodosDoAlbum(Album album)
        {
            return BuscarTodos().Where(foto => foto.Album.Id == album.Id);
        }


        public void CriarComId(Foto entity)
        {
            _fotoRepository.CreateWithId(entity);
        }
    }
}
