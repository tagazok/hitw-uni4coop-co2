export interface Reward {
  code: Code;
  distance: number | undefined;
  tripId: number;
}
export enum Code {
  VEGGIE = "VEGGIE",
  TRANSPORTATION = "TRANSPORTATION",
  SHOWER = "SHOWER",
  PLASTIC = "PLASTIC",
  COMPUTER = "COMPUTER",
  THERMOSTAT = "THERMOSTAT",
}
