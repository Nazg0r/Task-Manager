using ErrorOr;

namespace BusinessLogic.Errors
{
	public static class TaskErrors
	{
		public static Error TasksNotFound => 
			Error.Custom(ErrorTypes.NotFound, "Tasks.NotFound", $"Failed to get tasks.");

		public static Error TaskNotFound =>
			Error.Custom(ErrorTypes.NotFound, "Task.NotFound", $"Failed to get task.");
	}
}
