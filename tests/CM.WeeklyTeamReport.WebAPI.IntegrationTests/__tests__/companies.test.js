const frisby = require('frisby');
const { API_ENDPOINT } = require('../config');
const Joi = frisby.Joi;

describe('Companies', () => {
    //GET: API_ENDPOINT/api/Companies
    it('should return a list of companies', function () {
        return frisby
            .get(`${API_ENDPOINT}/api/Companies`)
            .expect('status', 200)
            .expect('jsonTypes', '*', {
                id: Joi.number().required(),
                name: Joi.string().required(),
                creationDate: Joi.string().allow(null)
            });
    });

    it('should CRUD a company', async () => {
        const result = await frisby
            .post(`${API_ENDPOINT}/api/Companies`, {
                Name: 'Aperture Science',
                CreationDate: new Date(Date.now()).toISOString()
            })
            .expect('status', 201)
            .expect('jsonTypes', {
                id: Joi.number().required(),
                name: Joi.string().required(),
                creationDate: Joi.string().allow(null)
            });
        const company = result.json;
        await frisby
            .get(`${API_ENDPOINT}/api/Companies/${company.id}`)
            .expect('status', 200)
            .expect('json', 'id', company.id)
            .expect('json', 'name', company.name)
            .expect('json', 'creationDate', company.creationDate);
        company.name = "Black Mesa";
        await frisby
            .put(`${API_ENDPOINT}/api/Companies/${company.id}`, {
                id: company.id,
                Name: company.name,
                CreationDate: company.creationDate
            })
            .expect('status', 200)
            .expect('json', 'id', company.id)
            .expect('json', 'name', company.name)
            .expect('json', 'creationDate', company.creationDate);
        await frisby
            .delete(`${API_ENDPOINT}/api/Companies/${company.id}`)
            .expect('status', 204);
    });
});