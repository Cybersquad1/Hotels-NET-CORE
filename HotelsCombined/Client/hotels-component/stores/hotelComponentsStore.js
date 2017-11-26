'use strict';

/**
 * @class HotelComponentsStore
 * @desc Object for storing HotelComponents to reuse their objects and html for newly fetched hotels
 * @property {Immmutable.List} imHotelComponents
 * @property {Immmutable} immutable
 */
class HotelComponentsStore {
    /**
     * @constructor HotelComponentsStore
     * @param {Immmutable} immutable
     */
    constructor(immutable) {
        this.immutable = immutable;
        this.imHotelComponents = null;
    }

    /**
     * @desc Stores an array of HotelComponents in an Immutable List object for quick access later
     * @param {Array<HotelComponent>} hotelComponents
     */
    save(hotelComponents) {

        this.imHotelComponents = this.immutable.fromJS(hotelComponents);
    };

     /**
     * @desc Updates existing HotelComponents with new hotels data
     * @param {Array<Hotel>} hotelComponents
     */
    update(newHotels)
    {
        if (!newHotels) return;

        //Create update hote components and update store state
        const newImHotelComponents = this.imHotelComponents.update(hotels => {

            let updatedHotels = null;
            //Loop through new hotels
            const hotelsLength = newHotels.length;
            if (hotelsLength < 1) return hotels;
            for (let i = 0; i < hotelsLength; i++)
            {
                const newhotel = newHotels[i];

                updatedHotels = hotels.update(i,hotelComponent => {
                    hotelComponent.update(newhotel);
                    return hotelComponent;
                });
            }
            return updatedHotels;
        });
        this.imHotelComponents = newImHotelComponents;
    }
}
export default HotelComponentsStore;
