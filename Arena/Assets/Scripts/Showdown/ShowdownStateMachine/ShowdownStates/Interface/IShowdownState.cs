#region File Header

/*******************************************************************************
 * Author: Matthew "Riktor" Baker
 * Filename: IBattleState.cs
 * Date Created: 4/12/2015 8:28PM EST
 * 
 * Description: The interface for the BattleState class.
 * 
 * Changelog:   - Modified: Matthew "Riktor" Baker - 4/12/2015 10:29 PM
 *              - Modified: Matthew "Riktor" Baker - 4/16/2015 9:01 PM - Added Comments
 *******************************************************************************/

#endregion

public interface IShowdownState
{
    /// <summary>
    /// Occurs when the state is left.
    /// </summary>
    void OnExit();

    /// <summary>
    /// Occurs when the state is entered.
    /// </summary>
    void OnEnter();

    /// <summary>
    /// Occurs everytime an update is called. (Once per frame in this instance)
    /// </summary>
    void Update();

    /// <summary>
    /// The showdown state type of the state. Used in battle state transitions.
    /// </summary>
    ShowdownState.ShowdownStateType StateType
    {
        get;
        set;
    }
}
