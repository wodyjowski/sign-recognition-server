export interface IPrediction {
  id: string;
  creationDate: Date;
  user: string;
  class: string;
  latitude: number;
  longitude: number;
  locationName?: string;
}
