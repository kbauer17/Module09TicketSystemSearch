public class Enhancement : Ticket
{
  public string software { get; set; }
  public string cost { get; set; }
  public string reason { get; set; }
  public UInt64 estimate { get; set; }
  

  // method to display tickets
  public override string Display()
    {
      return $"Id: {ticketId}\nSummary: {summary}\nStatus: {status}\nPriority: {priority}\nSubmitter: {submitter}\nAssigned: {assigned}\nWatcher: {watcher}\nSoftware: {software}\nCost: {cost}\nReason: {reason}\nEstimate: {estimate} weeks\n"; 
    }
}