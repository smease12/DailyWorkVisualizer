namespace DailyWorkVisualizer.Models;

public class ToDo
{
    public int Id {get; set;}
    public DateTime ToDoDate {get; set;}
    public string Description {get; set;}
    public bool isDone {get; set;}
    public bool isCurrentTask {get; set;}

    public string getStatus()
    {
        if(isDone)
            return "Done";
        else
            return "Not Done";
    }

    public string getStatusColor(){
        if(isDone)
            return "green";
        else
            return "red";
    }

}
