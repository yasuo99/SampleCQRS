﻿using AutoMapper;
using MediatR;
using SampleCQRSApplication.Data;
using SampleCQRSApplication.DTO;
using SampleCQRSApplication.Message;

namespace SampleCQRSApplication.Command
{
    public class AddOrUpdateTournamentTeamCommand : IRequest<IResultResponse>
    {
        public int Id { get; set; }
        public TournamentTeamRequest TournamentTeam { get; set; }
    }
    public class AddOrUpdateTournamentTeamCommandHandler : IRequestHandler<AddOrUpdateTournamentTeamCommand, IResultResponse>
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;

        public AddOrUpdateTournamentTeamCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
        }

        public async Task<IResultResponse> Handle(AddOrUpdateTournamentTeamCommand request, CancellationToken cancellationToken)
        {
            Team team = await unitOfWork.TeamRepository.GetByID(request.TournamentTeam.TeamId);
            Tournament tournament = await unitOfWork.TournamentRepository.GetByID(request.TournamentTeam.TournamentId);

            if (team == null || tournament == null)
                return ResultResponse.BuildResponse(0);

            if (request.Id == 0)
            {
                var temp = mapper.Map(request.TournamentTeam, new TournamentTeam());
                unitOfWork.TournamentTeamRepository.Insert(temp);
                await unitOfWork.Save();
                return ResultResponse.BuildResponse(temp.Id);
            }

            var tournamentTeam = unitOfWork.TournamentTeamRepository.Get(filter: x => x.Id == request.Id).FirstOrDefault();

            if (tournament == null)
            {
                return ResultResponse.BuildResponse(0);
            }

            unitOfWork.TournamentTeamRepository.Update(mapper.Map(request.TournamentTeam, tournamentTeam));
            await unitOfWork.Save();
            return ResultResponse.BuildResponse(tournamentTeam.Id);
        }
    }
}
