# PokéEffectiveness

A simple .NET console app that uses the [PokéAPI](https://pokeapi.co/) to determine a given Pokémon's type strengths and weaknesses.

## 🔍 What It Does

You provide the name of a Pokémon (e.g. `pikachu`, `charizard`), and the app fetches its type(s) from the PokéAPI. It then displays which other types it is strong or weak against based on official Pokémon type effectiveness rules.

## 🧰 Tech Stack

- .NET 9
- C#
- `System.Net.Http`
- `System.Text.Json`
- PokéAPI (`https://pokeapi.co/api/v2/pokemon/{name}`)

## 🚀 How to Run the App

### 1. Clone the Repository

```bash
git clone https://github.com/scottmsoule/pkteffectiveness.git
cd pkteffectiveness
