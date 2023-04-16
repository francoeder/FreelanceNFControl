export class ValidationHelper {

    static IsNullOrWhiteSpace(value: any): boolean {
        const options = [ undefined, null, '' ];
        return options.includes(value);
    }

}