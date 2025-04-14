# Reversi Game

A simple yet functional implementation of the classic **Reversi (Othello)** board game using **Windows Forms** in **C#**.

## 🕹️ About the Game

Reversi is a strategy board game for two players, played on an 8×8 uncheckered board. The game pieces have two sides: black and white. Players take turns placing their discs, flipping the opponent's discs by trapping them between two of their own.

## 🎮 Game Modes

- 👥 **Human vs Human** – Play locally with a friend.
- 🤖 **Human vs Computer** – Challenge a basic AI opponent.

## 📷 Screenshots


## 🛠 Features

- Classic 8x8 Reversi gameplay
- Two game modes: Player vs Player and Player vs AI
- Turn-based play with valid move logic
- Automatic disc flipping
- Game over detection and winner announcement
- Simple and clean UI built with Windows Forms

## 🚀 Getting Started

### Prerequisites

- [.NET Framework 4.8](https://dotnet.microsoft.com/en-us/download/dotnet-framework/net48)
- Visual Studio 2019 or later (recommended)

### Running the Project

1. Clone the repository:
   ```bash
   git clone https://github.com/AmirAbdollahi/reversi.git
   ```

2. Open the solution file `Reversi.sln` in Visual Studio.

3. Build and run the project using `F5` or from the **Debug** menu.

## 📁 Project Structure

```
Reversi/
├── GameBoard.cs           # Game logic and board state
├── Form1.cs               # Main Windows Form UI
├── Program.cs             # Entry point
├── Resources/             # Images or assets
└── Reversi.sln            # Solution file
```

## 📚 How to Play

1. Choose your preferred game mode at startup.
2. The black player starts first.
3. Each player must place a disc that flips at least one opponent disc.
4. Turns alternate until neither player can move.
5. The player with the most discs on the board wins.

## ✅ To-Do / Future Improvements

- Improve AI difficulty and strategy
- Implement game save/load features
- Enhance UI/UX design

## 🤝 Contributing

Contributions are welcome! Please feel free to submit pull requests or open issues for suggestions and improvements.

## 📄 License

This project is licensed under the [MIT License](LICENSE).

## 🙋‍♂️ Author

Developed by [Amir Abdollahi](https://github.com/AmirAbdollahi)

---

Enjoy playing Reversi, whether you're battling a friend or testing your skills against the computer!
