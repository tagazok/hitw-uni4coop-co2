import { Trip } from "./trip";

export interface Dashboard {
  totalCo2: number;
  trips: Trip[];
}
