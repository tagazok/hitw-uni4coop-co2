export interface Reward {
  code: Code;
  distance: number | undefined;
  amount: number | undefined;
  tripId: number;
  date?: string;
  creditInKgOfCo2?: number
}
export enum Code {
  VEGGIE = "VEGGIE",
  TRANSPORTATION = "TRANSPORTATION",
  SHOWER = "SHOWER",
  PLASTIC = "PLASTIC",
  COMPUTER = "COMPUTER",
  THERMOSTAT = "THERMOSTAT",
  DONATION = "DONATION",
  RECYCLING = "RECYCLING",
}
