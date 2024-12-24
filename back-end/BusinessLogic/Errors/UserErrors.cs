using ErrorOr;

namespace BusinessLogic.Errors
{
	public static class UserErrors
	{
		public static Error UserNotFound =>
			Error.Custom(ErrorTypes.NotFound, "User.NotFound", $"Failed to get user.");

		public static Error UserNotRemoved =>
			Error.Custom(ErrorTypes.NotRemoved, "User.NotRemoved", $"Failed to remove user.");

	}
}
