const frisby = require('frisby');
const { API_ENDPOINT } = require('../config');
const Joi = frisby.Joi;

describe('Team members', () => {
    //GET: API_ENDPOINT/api/Companies
    it('should return a list of team members', function () {
        return frisby
            .get(`${API_ENDPOINT}/api/Companies/${1}/Members`)
            .expect('status', 200)
            .expect('jsonTypes', '*', {
                id: Joi.number().required(),
                firstName: Joi.string().required(),
                lastName: Joi.string().required(),
                title: Joi.string().required(),
                email: Joi.string().required(),
                companyId: Joi.number().required()
            });
    });

    it('should CRUD a team member', async () => {
        // Company setup
        let result = await frisby
            .post(`${API_ENDPOINT}/api/Companies`, {
                Name: 'Some model agency',
                CreationDate: new Date(Date.now()).toISOString()
            });
        const company = result.json;

        // Team member create
        result = await frisby
            .post(`${API_ENDPOINT}/api/Companies/${company.id}/Members`, {
                FirstName: 'Mai',
                LastName: 'Sakurajima',
                Title: 'Actress',
                Email: 'mai@agency.com',
                CompanyId: company.id
            })
            .expect('status', 201)
            .expect('jsonTypes', {
                id: Joi.number().required(),
                firstName: Joi.string().required(),
                lastName: Joi.string().required(),
                title: Joi.string().required(),
                email: Joi.string().required(),
                companyId: Joi.number().required()
            });
        const tm = result.json;

        // Team member read
        await frisby
            .get(`${API_ENDPOINT}/api/Companies/${company.id}/Members/${tm.id}`)
            .expect('status', 200)
            .expect('json', 'id', tm.id)
            .expect('json', 'firstName', tm.firstName)
            .expect('json', 'lastName', tm.lastName)
            .expect('json', 'title', tm.title)
            .expect('json', 'email', tm.email)
            .expect('json', 'companyId', tm.companyId);

        // Team member update
        tm.lastName = "Azusagawa";
        await frisby
            .put(`${API_ENDPOINT}/api/Companies/${company.id}/Members/${tm.id}`, {
                FirstName: tm.firstName,
                LastName: tm.lastName,
                Title: tm.title,
                Email: tm.email,
                CompanyId: company.id
            })
            .expect('status', 200)
            .expect('json', 'id', tm.id)
            .expect('json', 'lastName', tm.lastName);

        // Team member delete
        await frisby
            .delete(`${API_ENDPOINT}/api/Companies/${company.id}/Members/${tm.id}`)
            .expect('status', 204);

        // Company clean
        await frisby
            .delete(`${API_ENDPOINT}/api/Companies/${company.id}`);
    });
});