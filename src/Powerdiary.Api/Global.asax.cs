using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Powerdiary.Contracts.Commands;
using Powerdiary.Contracts.Events;
using Powerdiary.Domain.Aggregates;
using Powerdiary.Domain.Projections;
using Powerdiary.Domain.Projections.EventsCountByOneMinute;
using Powerdiary.Domain.Projections.EventsCountExtendedByFourMinute;
using Powerdiary.Domain.Projections.EventsCountDetailedByOneMinute;
using Powerdiary.Domain.Projections.EventsWholeHistory;
using Powerdiary.Infrastructure;

namespace Powerdiary.Api
{
	public class WebApiApplication : System.Web.HttpApplication
	{
		protected void Application_Start()
		{
			AreaRegistration.RegisterAllAreas();
			GlobalConfiguration.Configure(WebApiConfig.Register);
			FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
			RouteConfig.RegisterRoutes(RouteTable.Routes);
			BundleConfig.RegisterBundles(BundleTable.Bundles);

			var bus = new FakeBus();
			var storage = new EventStore(bus);
			var rep = new Repository<ChatRoom>(storage);
			var commands = new ChatRoomCommandHandlers(rep);

			bus.RegisterHandler<CreateChatRoom>(commands.Handle);
			bus.RegisterHandler<RenameChatRoom>(commands.Handle);
			bus.RegisterHandler<SendFive>(commands.Handle);
			bus.RegisterHandler<SendComment>(commands.Handle);
			bus.RegisterHandler<EnterChatRoom>(commands.Handle);
			bus.RegisterHandler<ExitChatRoom>(commands.Handle);

			var list = new ChatsListView();
			bus.RegisterHandler<ChatRoomCreated>(list.Handle);
			bus.RegisterHandler<ChatRoomRenamed>(list.Handle);

			var eventsCountDetailedByOneMinuteProjection = new EventsCountDetailedByOneMinuteProjection();
			bus.RegisterHandler<FiveSent>(eventsCountDetailedByOneMinuteProjection.Handle);
			bus.RegisterHandler<CommentSent>(eventsCountDetailedByOneMinuteProjection.Handle);
			bus.RegisterHandler<UserEntered>(eventsCountDetailedByOneMinuteProjection.Handle);
			bus.RegisterHandler<UserExited>(eventsCountDetailedByOneMinuteProjection.Handle);

			var eventsCountExtendedByFourMinuteProjection = new EventsCountExtendedByFourMinuteProjection();
			bus.RegisterHandler<FiveSent>(eventsCountExtendedByFourMinuteProjection.Handle);
			bus.RegisterHandler<CommentSent>(eventsCountExtendedByFourMinuteProjection.Handle);
			bus.RegisterHandler<UserEntered>(eventsCountExtendedByFourMinuteProjection.Handle);
			bus.RegisterHandler<UserExited>(eventsCountExtendedByFourMinuteProjection.Handle);

			var eventsCountByOneMinuteProjection = new EventsCountByOneMinuteProjection();
			bus.RegisterHandler<FiveSent>(eventsCountByOneMinuteProjection.Handle);
			bus.RegisterHandler<CommentSent>(eventsCountByOneMinuteProjection.Handle);
			bus.RegisterHandler<UserEntered>(eventsCountByOneMinuteProjection.Handle);
			bus.RegisterHandler<UserExited>(eventsCountByOneMinuteProjection.Handle);

			var eventsWholeHistoryProjection = new EventsWholeHistoryProjection();
			bus.RegisterHandler<FiveSent>(eventsWholeHistoryProjection.Handle);
			bus.RegisterHandler<CommentSent>(eventsWholeHistoryProjection.Handle);
			bus.RegisterHandler<UserEntered>(eventsWholeHistoryProjection.Handle);
			bus.RegisterHandler<UserExited>(eventsWholeHistoryProjection.Handle);

			ServiceLocator.Bus = bus;
        }
	}
}
