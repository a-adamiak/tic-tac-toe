namespace TicTacToe.Api.Dto
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="Id">Id of the game</param>
    /// <param name="Cells">Game board with the marked cells</param>
    /// <param name="Status">Current status of the game</param>
    /// <param name="ClientMark">Initial client mark</param>
    public record GameDto(Guid Id, TagDto?[][] Cells, GameStatusDto Status, TagDto ClientMark)
    {
    }

    public enum GameStatusDto
    {
        /// <summary>
        /// Game is still in the progress
        /// </summary>
        InProgress,
        /// <summary>
        /// Client won the game
        /// </summary>
        ClientWon,
        /// <summary>
        /// Bot won the game
        /// </summary>
        BotWon,
        /// <summary>
        /// Game finished without any winner
        /// </summary>
        Draw,
        /// <summary>
        /// Game failed and could not be continued 
        /// </summary>
        Failed
    }

    public enum TagDto
    {
        X,
        O
    }
}
