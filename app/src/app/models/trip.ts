export interface Trip {
  id?: number;
  label: string;
  departure: string;
  arrival: string;
  isRoundTrip: boolean;
  percentage: number;
  co2: number;
}
