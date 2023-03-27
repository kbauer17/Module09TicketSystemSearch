public class Task : Ticket
{
  public string projectName { get; set; }
  public string dueDate { get; set; }
  

  // method to display tickets
  public override string Display()
    {
      return $"Id: {ticketId}\nSummary: {summary}\nStatus: {status}\nPriority: {priority}\nSubmitter: {submitter}\nAssigned: {assigned}\nWatcher: {watcher}\nProject Name: {projectName}\nDue Date: {dueDate}\n"; 
    }
}