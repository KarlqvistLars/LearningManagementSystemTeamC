using LearningManagementSystemTeamC.Application.ChatRooms;
using LearningManagementSystemTeamC.Application.Common.DTOs;
using LearningManagementSystemTeamC.Application.Common.Interfaces;

namespace LearningManagementSystemTeamC.Application.Messages.Queries.GetMessages;

public class GetMessagesHandler : IGetMessagesHandler
{
    private readonly IChatRoomRepository _chatRoomRepository;
    private readonly IMessageRepository _messageRepository;
    private readonly IUnitOfWork _unitOfWork;

    public GetMessagesHandler(IChatRoomRepository chatRoomRepository, IMessageRepository messageRepository, IUnitOfWork unitOfWork)
    {
        _chatRoomRepository = chatRoomRepository;
        _messageRepository = messageRepository;
        _unitOfWork = unitOfWork;
    }

    public Task<MessageDto> HandleAsync(GetMessagesQuery query, Guid currentUserId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}
