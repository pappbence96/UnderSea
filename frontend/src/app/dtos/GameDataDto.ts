export class GameDataDto {
    roundNumber: number;
    scoreboardPosition: number;
    username: string;
    countryName: string;
    pearl: number;
    coral: number;
    pearlPerRound: number;
    coralPerRound: number;
    population: number;
    units: Map<number, number>;
    buildings: Map<number, number>;
}