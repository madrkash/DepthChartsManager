﻿using System;
using DepthChartsManager.Common.Request;
using FluentValidation;

namespace DepthChartsManager.ConsoleApp.Validators
{
	public class RemovePlayerRequestValidator : AbstractValidator<RemovePlayerRequest>
	{
		public RemovePlayerRequestValidator()
		{
			RuleFor(x => x.Id).NotEmpty();
            RuleFor(x => x.LeagueId).NotEmpty();
            RuleFor(x => x.Name).NotEmpty();
            RuleFor(x => x.Position).NotEmpty();
            RuleFor(x => x.TeamId).NotEmpty();
        }
	}
}

