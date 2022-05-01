import {GameStatus} from "../enums";

export const notifyOnStatusChanged = (status: GameStatus): void => {
    switch (status){
        case GameStatus.Failed:
            alert('Sorry but your game failed. Please open a new game');
            break
        case GameStatus.Draw:
            alert('You tied. Next time you will definitely succeed!');
            break
        case GameStatus.BotWon:
            alert('Oops. You have lost. Next time try harder.');
            break
        case GameStatus.ClientWon:
            alert('Congratulations. You won. Keep it up !');
            break
    }
}
