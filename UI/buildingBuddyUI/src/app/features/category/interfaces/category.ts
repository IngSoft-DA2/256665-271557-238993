export interface Category
{
    id: string,
    name : string,
    categoryFatherId? : string,
    subCategories? : Category[]
};
