
public class TopicSave 
{

    public int ID;

    public QuestionDifficultyIntValue[] QuestionDifficultyToRewardArray;

    public QuestionDifficultyBoolValue[] QuestionDifficultyToLockedStatusArray;

    public QuestionDifficultyBoolValue[] QuestionDifficultyToCompletedStatusArray;


    public TopicSave(Topic topic)
    {
        ID = topic.ID;
        QuestionDifficultyToRewardArray = topic.QuestionDifficultyToRewardArray;
        
        QuestionDifficultyToLockedStatusArray = topic.QuestionDifficultyToLockedStatusArray;

        QuestionDifficultyToCompletedStatusArray = topic.QuestionDifficultyToCompletedStatusArray;



    }

}
