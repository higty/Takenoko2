/*
 * Created by Ivan Sanz (@isc30)
 * Copyright © 2017 Ivan Sanz Carasa. All rights reserved.
*/
/*export function Lazy<T>(factory: () => T): () => T
{
    let instance: T;
    
    return () => instance !== undefined
        ? instance
        : (instance = factory());
}*/
export class Cached {
    constructor() {
        this._isValid = false;
    }
    invalidate() {
        this._isValid = false;
    }
    isValid() {
        return this._isValid;
    }
    get value() {
        if (!this._isValid) {
            throw new Error("Trying to get value of invalid cache");
        }
        return this._value;
    }
    set value(value) {
        this._value = value;
        this._isValid = true;
    }
}
//# sourceMappingURL=Utils.js.map